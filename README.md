# dapr-calculator

# rest-calculator-linkerd
This sample application is intended to help .NET developers has been developed to show case the differences between Dapr and Service Meshes. An equivalent calculator has been designed for Linkerd with both REST and gRPC (explore my repos to find them).
The app is a calculator where each service is a single mathematical operation:
 * Division: this one throws from time to time a division by zero to let LinkerD catch and report them
 * Multiplication
 * Addition
 * Substraction
 * Percentage: this one calls Multiplication followed by Division
 
The MathFanBoy console generates traffic so as to randomly call one of these mathematical operations. 

This app supports my blog post [Decoding the Dynamics: Dapr vs. Service Meshes](https://techcommunity.microsoft.com/t5/blogs/blogworkflowpage/blog-id/AzureDevCommunityBlog/article-id/1113) where I depict the major differences between Dapr and Service Meshes.

# Prerequisites
To get this demo app up & running, you need the following:
* A K8s cluster. 
* Install Dapr: read their [documentation](https://docs.dapr.io/operations/hosting/kubernetes/kubernetes-deploy/) to get started. 

# Deploy to AKS (or any other K8s cluster)
All the container images are hosted on my personal Docker Hub repos, so you don't need to build anything yourself if you just want to see this in action. Just make sure to comply with the prerequisites.

```
curl -sL https://github.com/stephaneey/dapr-calculator-linkerd/blob/master/dapr-calculator.yaml \
  | kubectl apply -f -  
```
