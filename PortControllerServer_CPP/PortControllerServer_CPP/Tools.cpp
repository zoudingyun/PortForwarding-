#include <Tools.h>


using namespace std;

std::vector<std::string> s_split(const std::string& in, const std::string& delim)
{
	std::regex re{ delim };
	// 调用 std::vector::vector (InputIterator first, InputIterator last,const allocator_type& alloc = allocator_type())
	// 构造函数,完成字符串分割
	return std::vector<std::string> {
		std::sregex_token_iterator(in.begin(), in.end(), re, -1),
			std::sregex_token_iterator()
	};
}

string getRealChar(char * chars) 
{
	string a = "";
	for (size_t i = 0; i < strlen(chars)&chars[i]!='\n'; i++)
	{
		a += chars[i];
	}

	return a;
}

map<string, string> getMessages(string message)
{
	map<string, string> hashtable ;
	auto s_result = s_split(message,"[|]");
	int aa = s_result.size();
	string* messsages = new string[aa];//new messsages
	if (sizeof(s_result) <=0)
	{
		return hashtable;
	}
	for (size_t i = 0; i < s_result.size(); i++)
	{
		messsages[i] = s_result[i];
	}
	try
	{
		for (size_t i = 0; i < s_result.size(); i++)
		{
			auto s_result1 = s_split(messsages[i], ":");
			if (s_result1.size() < 2) {
				continue;
			}
			else {
				string messsagesTmp[2];
				for (size_t i = 0; i < s_result1.size(); i++)
				{
					messsagesTmp[i] = s_result1[i];
				}
				string key = messsagesTmp[0];
				string value = messsagesTmp[1];
				hashtable.insert({ key, value });
			}
		}
		delete[] messsages;//delete messsages
		return hashtable;
	}
	catch (const std::exception a)
	{
		cout << a.what() << endl;
		delete[] messsages;
		return hashtable;
	}
	
}

