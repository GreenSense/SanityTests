pipeline {
    agent any
    properties([pipelineTriggers([cron('2 * * *')])])
    stages {
        stage('Init') {
            steps {
                sh 'sh init.sh'
            }
        }
        stage('Build') {
            steps {
                sh 'sh build.sh'
            }
        }
        stage('Test') {
            steps {
                sh 'sh test.sh'
            }
        }
    }
    post {
        always {
            cleanWs()
        }
    }
}
