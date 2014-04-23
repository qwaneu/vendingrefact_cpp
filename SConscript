env = Environment(
  CPPPATH = ['/usr/include','include','ext'],
  CXXFLAGS="-std=c++0x")

gtest_env = env.Clone()
gtest_env.Append(LINKFLAGS=['-pthread', '-Wall', '-g'])

SOURCES       = Glob('src/*.cpp')
GTEST_SOURCES = Glob('ext/gtest/*.cc')
TEST_SOURCES  = Glob('test/*.cpp')

unit_tests=gtest_env.Program(target='unit_tests', 
    source=SOURCES + GTEST_SOURCES + TEST_SOURCES)

gtest_env.AlwaysBuild(gtest_env.Alias('test', [unit_tests], unit_tests[0].abspath))

