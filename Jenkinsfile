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
	dockerImage = ''  
	registry = 'mariuslp234/tidpp-lab4'
	registryCredential = 'dockerhub_id'
    }
    
    stages {
        stage ('Git checkout'){
            steps{
                git branch: 'master', credentialsId: '$ACCESS_ID' , url: 'https://github.com/MariusLupasco/TIDPP-Lab3.git'
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
                bat 'dotnet test eTickets.sln -l:junit;LogFileName=C:\\JenkinsWS\\workspace\\eTicketsProject\\UnitTests\\TestResults'               
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
	stage('Continuous Delivery'){
	    steps {
		script {
		    dockerImage = docker.build registry	 
		}
	    }
	}
	stage('Image Uploading'){
		steps {
			script {
				docker.withRegistry( '', registryCredential){
					dockerImage.push()
				}
			}
		}
	}
	 // Continuous Deployment Placeholder   
	stage ('Continuous Deployment'){
            steps {
                script {
                    withCredentials([sshUserPrivateKey(credentialsId: 'vbox-id', keyFileVariable: 'identity', passphraseVariable: '', usernameVariable: 'user')]){
                        def remote = [:]
                        remote.name = 'user'
                        remote.host = '192.168.56.101'
                        remote.user = 'user'
                        remote.identityFile = identity
                        remote.allowAnyHosts = true
                        
                        sshCommand remote: remote, command: 'cd ~/tidpp && echo 1234 | sudo -S docker-compose up -d'
                        
                    }
                }
            }
        }
    }
    
    post {
        
        always {
            
            emailext attachLog: true, body: 'Result $BUILD_URL', subject: '$JOB_NAME - Build # $BUILD_NUMBER - $BUILD_STATUS', to: 'testing.jenkins1@gmail.com'
            
            echo "${BUILD_TAG}"
            
            junit '**/UnitTests/TestResults/*.xml'
            
	   
		
            script {
                if (params.CLEAN_WORKSPACE == true){
                    cleanWs()
		     bat "docker rmi $registry"
                }
            }
        }
	success {
		echo "${JOB_NAME} - ${BUILD_NUMBER} ran successfully"
	}
        failure {
		echo "${JOB_NAME} - ${BUILD_NUMBER} failed"
	}
	unstable {
		echo "${JOB_NAME} - ${BUILD_NUMBER} is unstable. Try to fix it."
	}
    }
}
