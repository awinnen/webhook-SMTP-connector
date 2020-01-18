pipeline {
    agent any
	
    stages {
        stage('build') {
			steps {
				sh 'docker build -t awinnen/webhook-smtp-connector:${GIT_REVISION,length=6} .'
			}
        }
		stage('deploy') {
			when {
				branch 'master'
			}
			steps {
				withCredentials([usernamePassword(credentialsId: 'hub-docker-com', usernameVariable: 'DOCKER_USERNAME', passwordVariable: 'DOCKER_PASSWORD')]) {
					sh "docker login -u ${DOCKER_USERNAME} -p ${DOCKER_PASSWORD}";
					sh "docker tag awinnen/webhook-smtp-connector:${GIT_REVISION,length=6} awinnen/webhook-smtp-connector:latest"
					sh "docker push awinnen/webhook-smtp-connector:${GIT_REVISION,length=6}" 
					sh "docker push awinnen/webhook-smtp-connector:latest" 
				}
			}
		}
    }
}