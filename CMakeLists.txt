cmake_minimum_required(VERSION 3.16)
project(VendingRefact)

set(CMAKE_CXX_STANDARD 20)

include_directories(.)
add_subdirectory(src)
add_subdirectory(test)
add_subdirectory(ext/googletest)
