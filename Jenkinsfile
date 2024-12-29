pipeline {
    agent any
    triggers {
        pollSCM('H/5 * * *') 
    }
    stages {
        stage('Initialize') {
            steps {
                script {
                    CODE_CHANGES = getGitChanges()
                }
            }
        }
        stage('Build') {
            when {
                expression {
                    return CODE_CHANGES == true
                }
            }
            steps {
                echo 'Building the application...'
                sh 'dotnet build Selenium_FirstTask.sln --configuration Release'
            }
        }
        stage('Test') {
            when {
                expression {
                    return CODE_CHANGES == true
                }
            }
            steps {
                echo 'Running tests...'
                sh 'dotnet test Selenium_FirstTask.csproj --configuration Release --logger "trx;LogFileName=test_results.trx"'
                junit '**/TestResults/*.trx'
            }
        }
        stage('Deploy') {
            when {
                expression {
                    return CODE_CHANGES == true
                }
            }
            steps {
                echo 'Deploying the application...'
            }
        }
    }
}

def getGitChanges() {
    def changeLogSets = currentBuild.changeSets
    def hasChanges = false
    for (change in changeLogSets) {
        for (entry in change.items) {
            echo "Changed by: ${entry.author} on ${new Date(entry.timestamp)}"
            echo "Message: ${entry.msg}"
            hasChanges = true
        }
    }
    return hasChanges
}