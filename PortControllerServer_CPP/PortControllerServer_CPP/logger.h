// logger.h
#pragma once
#include <string>
#include <fstream>
#include <sstream>
#include <mutex>

struct tm;

namespace logger
{
	// 强类型枚举，指定日志等级
	enum class Level { Debug, Info, Warning, Error, Fatal };
	class FileLogger; // 写文档用的日志类
	class ConsoleLogger; // 控制台输出用的日志类
	class BaseLogger; // 纯虚基类

	class BaseLogger
	{
		class LogStream; // 用于文本缓冲的内部类声明
	public:
		BaseLogger() = default;
		virtual ~BaseLogger() = default;
		// 重载 operator() 返回缓冲区对象
		virtual LogStream operator()(Level nLevel = Level::Debug);
	private:
		const tm* getLocalTime();
		// 供缓冲区对象析构时调用（函数加锁保证线程安全）
		void endline(Level nLevel, std::string&& oMessage);
		// 纯虚函数，预留接口，由派生类实现
		virtual void output(const tm *p_tm,
			const char *str_level,
			const char *str_message) = 0;
	private:
		std::mutex _lock;
		tm _localTime;
	};

	// 用于文本缓冲区的类，继承 std::ostringstream
	class BaseLogger::LogStream : public std::ostringstream
	{
		BaseLogger& m_oLogger;
		Level        m_nLevel;
	public:
		LogStream(BaseLogger& oLogger, Level nLevel)
			: m_oLogger(oLogger), m_nLevel(nLevel) {};
		LogStream(const LogStream& ls)
			: m_oLogger(ls.m_oLogger), m_nLevel(ls.m_nLevel) {};
		~LogStream() // 为其重写析构函数，在析构时打日志
		{
			m_oLogger.endline(m_nLevel, std::move(str()));
		}
	};

	// 控制台输出用的日志类
	class ConsoleLogger : public BaseLogger
	{
		using BaseLogger::BaseLogger;
		virtual void output(const tm *p_tm,
			const char *str_level,
			const char *str_message);
	};

	// 写文档用的日志类
	class FileLogger : public BaseLogger
	{
	public:
		FileLogger(std::string filename) noexcept;
		FileLogger(const FileLogger&) = delete;
		FileLogger(FileLogger&&) = delete;
		virtual ~FileLogger();
	private:
		virtual void output(const tm *p_tm,
			const char *str_level,
			const char *str_message);
	private:
		std::ofstream _file;
	};

	extern ConsoleLogger debug;
	extern FileLogger record;

} // namespace logger
