#pragma once
#ifndef TOOLS_H
#define TOOLS_H

#include<iostream>
#include <vector>
#include <iterator>
#include <regex>
#include<map>

using namespace std;

std::vector<std::string> s_split(const std::string& in, const std::string& delim);
std::string getRealChar(char * chars);
map<string, string> getMessages(string message);


#endif