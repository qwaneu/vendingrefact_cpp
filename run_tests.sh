#!/bin/bash

cd build
cmake .. && cmake --build . && test/VendingRefact_tst
