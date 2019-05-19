pipeline {
    //Use the following docker image to run your dotnet app.
    agent { docker { image 'mcr.microsoft.com/dotnet/core/sdk:2.2-alpine' } }
    environment {HOME = '/tmp'} 
    stages {
    // Get some code from a GitHub repository
    stage('Git') {
      steps{
          git 'https://github.com/ItayFrid/SocialNetwork.git'
      }
   }
    stage('Dotnet Restore'){
        steps{
        sh "nuget restore packages.config -PackagesDirectory Packages"
        }
    }
    
   stage('Build'){
          steps{
               sh "dotnet build SocialNetwork.sln"
               }
    }
    stage('Run Tests'){
          steps{
               sh "dotnet test"
          }
    }
}
}
