pipeline {
    agent any
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
            }
        }
        stage('Test') {
            steps {
                echo 'Testing the application...'
            }
        }
        stage('Deploy') {
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
