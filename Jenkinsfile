pipeline {
    agent any
    
    options {
        timestamps()
    }
    
    parameters {
        booleanParam(name: 'CLEAN_WORKSPACE', defaultValue: true, description: 'Boolean parameter for cleaning the workspace')
        booleanParam(name: 'TESTING_FRONTEND', defaultValue: true, description: 'Boolean parameter for triggering the Frontend test')
    }
    
    environment {
        ON_SUCCESS_SEND_EMAIL = 'true'
        ON_FAILURE_SEND_EMAIL = 'true'
        ACCESS_ID = credentials('mariusId')
        
    }
    
    stages {
        stage ('Clean workspace'){
            steps {
                cleanWs()
            }
        }
        stage ('Git checkout'){
            steps{
                git branch: 'master', credentialsId: '$ACCESS_ID' , url: 'https://github.com/MariusLupasco/eTicketsProject.git'
            }
        }
        stage ('Restore dependencies'){
            steps {
                bat 'dotnet restore eTickets.sln'
            }
        }
        stage ('Build'){
            steps {
                echo "Build number ${BUILD_NUMBER} and ${BUILD_TAG}"
                bat 'msbuild eTickets.sln /nologo /nr:false'
            }
        }
        stage ('Running Unit Tests'){
            steps {
                bat 'dotnet test eTickets.sln'
            }
        }
        stage ('Testing Frontend'){
            steps {
                script {
                    if(params.TESTING_FRONTEND == true){
                        echo 'Running Frontend tests'
                    } else {
                        echo 'Not running Frontend tests'
                    }
                }
            }
        } 
    }
}
