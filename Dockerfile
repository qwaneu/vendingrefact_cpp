FROM 525595969507.dkr.ecr.eu-central-1.amazonaws.com/qwan/course-cpp:latest

COPY --chown=experience:experience ./ vendingrefact
RUN cd vendingrefact &&\
    git init . && git add . && git commit -am "initial commit" &&\
    git remote add qwan /git/qwan/vendingrefact

# test - this will fail with a test failure
 RUN cd vendingrefact && (./run_tests.sh || echo "test failed as expected")
