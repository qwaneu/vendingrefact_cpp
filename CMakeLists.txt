cmake_minimum_required(VERSION 3.13)
project(VendingRefact)

set(CMAKE_CXX_STANDARD 14)

include_directories(.)
add_subdirectory(src)
add_subdirectory(test)
add_subdirectory(ext/googletest)
