pipeline {
    agent any
	
	environment {
		GIT_COMMIT_SHORT = "${GIT_COMMIT[0..7]}"
	}
	
    stages {
        stage('build') {
			steps {
				sh "docker build -t awinnen/webhook-smtp-connector:$GIT_COMMIT_SHORT ./webhook-SMTP-connector"
			}
        }
		stage('deploy') {
			when {
				branch 'master'
			}
			steps {
				withCredentials([usernamePassword(credentialsId: 'hub-docker-com', usernameVariable: 'DOCKER_USERNAME', passwordVariable: 'DOCKER_PASSWORD')]) {
					sh "docker login -u ${DOCKER_USERNAME} -p ${DOCKER_PASSWORD}";
					sh "docker tag awinnen/webhook-smtp-connector:$GIT_COMMIT_SHORT awinnen/webhook-smtp-connector:latest"
					sh "docker push awinnen/webhook-smtp-connector:$GIT_COMMIT_SHORT" 
					sh "docker push awinnen/webhook-smtp-connector:latest" 
				}
			}
		}
    }
}