CODE_CHANGES == getGitChanges()
pipeline {
    agent any
    stages {
        stage('Build') {
            when {
                expression {
                    CODE_CHANGES == true
                }
            }
            steps {
                echo 'building the application...'
            }
        }
        stage('Test') {
            steps {
                echo 'testing the application...'
            }
        }
        stage('Deploy') {
            steps {
                echo 'deploying the application...'
            }
        }
    }
}
