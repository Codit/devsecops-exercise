# DevSecOps Challenge

This GitHub repo contains a REST API which provides information about storage.

It's up to you to :

- Build, tag & push container to an Azure Container Registry _(Based on [our Dockerfile](https://github.com/CoditEU/devsecops-exercise/blob/master/src/Codit.Exercises.DevSecOps.API/Dockerfile))_
- Deploy API on Azure Web App for Containers with Azure Pipelines

Once that's done, we suggest to pick one of these tasks:

- Automated vulnerability scanning in the deployment pipeline
- Secure with Azure Application Gateway (WAF)
- Secure with 42Crunch

## Bonus points

Already done? Here are some more tasks as bonus points:

- Automated vulnerability scanning in the release pipeline _(if not done already)_
- Secure with Azure Application Gateway (WAF) _(if not done already)_
- Secure with 42Crunch _(if not done already)_
- Run our smoke tests in the deployment pipeline after deployment to Azure Web App for Containers
- Provide alerts for vulnerable images in our container registry