Docker Deployment
=================

This guide is a quick intro into the commands to use for deploying a docker container.

Step 1
------

Build the image.  The image contains all the details on server dependencies, ports and any other scripts ran. 

NOTE: Future request would be to pass in the tag name of the github check-in you want to deploy.  For instance, if you want to deploy v0.1-Beta.

```
docker build -t people-search .
```


Step 2
------

This step starts up the container.  -d is detached, -p is the port exposed on container:host, --name is for the name of the container and people-search is the name of the image to use.
```
docker run -d -p 8000:8000 --name=ps people-search
```