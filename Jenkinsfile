pipeline {
    agent any

    stages {
        stage('Restore Dependencies') {
            steps {
                echo 'Restoring dependencies...'
                sh 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                echo 'Building the project...'
                sh 'dotnet build --configuration Release'
            }
        }

        stage('Run Tests') {
            steps {
                echo 'Running tests...'
                sh 'dotnet test --configuration Release --logger "trx;LogFileName=test_results.trx"'
            }
        }
    }

    post {
        always {
            echo 'Pipeline execution completed.'
            archiveArtifacts artifacts: '**/test_results.trx', allowEmptyArchive: true
        }
    }
}
