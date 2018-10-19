#include<iostream>
#include<WinSock2.h>
#include <vector>
#include <iterator>
#include <regex>
#include <Tools.h>
#include <string>
#include <thread>
#include "INIParser.h"


#pragma comment(lib, "ws2_32.lib")

#define CONNECT_NUM_MAX 10
#define BUF_NUM 10240
#define LOGIN "LOGIN"
#define CONNECT "CONNECT"
#define CHANGPWD "CHANGPWD"
#define CHANGENAME "CHANGENAME"
#define RESET_USER_PWD "RESET-USER-PWD"

using namespace std;


void output(int i,int j);
SOCKET listen(int port);





int main() 
{
	//auto s_result = s_split("hello,do you ;know the word?", ",");
	//std::copy(s_result.begin(), s_result.end(), std::ostream_iterator<std::string>(std::cout, "\n"));

	/*for (uint8_t i = 0; i < 4; i++)
	{
		thread t(output, i,10000);
		t.detach();

	}
	
	getchar();
	return 0;*/
	int port = 8007;
	INIParser ini_parser;//读取配置文件




	try
	{
		while (true)
		{
			SOCKET clientSocket = listen(port);
			if (clientSocket == NULL)
			{
				continue;
			}
			////接收第一次请求数据
			char recvBuf[BUF_NUM];
			recv(clientSocket, recvBuf, BUF_NUM, 0);
			string userMessage = getRealChar(recvBuf);
			//delete(recvBuf);
			map<string, string> messageMap =  getMessages(userMessage);

			if ((string)messageMap["TYPE"] == LOGIN)
			{
				float ver = stof((string)messageMap["VER"]);
				ini_parser.ReadINI("conf.ini");
				ini_parser.WriteINI("conf.ini");

				ini_parser.SetValue("class1", "name1", "Tom");
				ini_parser.SetValue("class2", "name2", "Lucy");
				ini_parser.WriteINI("test.ini");
				//if(ver)
				Sleep(100);
			}
			else if((string)messageMap["TYPE"] == CONNECT)
			{

			}
			else if((string)messageMap["TYPE"] == CHANGPWD)
			{

			}
			else if ((string)messageMap["TYPE"] == CHANGENAME)
			{

			}
			else
			{

			}


		}
	}
	catch (const std::exception a)
	{
		cout << a.what() <<endl;
	}
}


void output(int i, int j)
{
	cout << i+j << endl;
}

SOCKET listen(int port)
{
	//加载套接字库
	WSADATA wsaData;
	int iRet = 0;
	iRet = WSAStartup(MAKEWORD(2, 2), &wsaData);
	if (iRet != 0)
	{
		cout << "WSAStartup(MAKEWORD(2, 2), &wsaData) execute failed!" << endl;;
		return NULL;
	}
	if (2 != LOBYTE(wsaData.wVersion) || 2 != HIBYTE(wsaData.wVersion))
	{
		WSACleanup();
		cout << "WSADATA version is not correct!" << endl;
		return NULL;
	}

	//创建套接字
	SOCKET serverSocket = socket(AF_INET, SOCK_STREAM, 0);
	if (serverSocket == INVALID_SOCKET)
	{
		cout << "serverSocket = socket(AF_INET, SOCK_STREAM, 0) execute failed!" << endl;
		return NULL;
	}

	//初始化服务器地址族变量
	SOCKADDR_IN addrSrv;
	addrSrv.sin_addr.S_un.S_addr = htonl(INADDR_ANY);
	addrSrv.sin_family = AF_INET;
	addrSrv.sin_port = htons(port);

	//绑定
	iRet = ::bind(serverSocket, (SOCKADDR*)&addrSrv, sizeof(SOCKADDR));
	if (iRet == SOCKET_ERROR)
	{
		cout << "bind(serverSocket, (SOCKADDR*)&addrSrv, sizeof(SOCKADDR)) execute failed!" << endl;
		return NULL;
	}


	//监听
	iRet = listen(serverSocket, CONNECT_NUM_MAX);
	if (iRet == SOCKET_ERROR)
	{
		cout << "listen(serverSocket, 10) execute failed!" << endl;
		return NULL;
	}

	//等待连接_接收_发送
	SOCKADDR_IN clientAddr;
	int len = sizeof(SOCKADDR);

		SOCKET connSocket = accept(serverSocket, (SOCKADDR*)&clientAddr, &len);
		if (connSocket == INVALID_SOCKET)
		{
			cout << "accept(serverSocket, (SOCKADDR*)&clientAddr, &len) execute failed!" << endl;
			return NULL;
		}

		////接收数据
		//char recvBuf[BUF_NUM];
		//recv(connSocket, recvBuf, 100, 0);
		//printf("%s\n", recvBuf);

		////发送数据
		//char sendBuf[BUF_NUM];
		//sprintf_s(sendBuf, "Welcome %s", inet_ntoa(clientAddr.sin_addr));
		//send(connSocket, sendBuf, strlen(sendBuf) + 1, 0);

		//closesocket(connSocket);
	

	return connSocket;
}