pipeline {
    //Use the following docker image to run your dotnet app.
    agent { docker { image 'mcr.microsoft.com/dotnet/framework/aspnet' } }
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
        sh "dotnet restore"
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
