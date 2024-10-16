# Common commands
## Initializing a new component
```
pac pcf init --namespace NumberedListNamespace --name NumberedListControl --template field
```



### Deploying a component
Select your organization and environment
```
pac org select --environment d2694529-ce5a-43d1-b7a4-785bcf25b448
```
Build a package (npm run package)
```
pcf-scripts build --package
```

Or manually build package
```
pac solution add-reference --path ./path-to-your-control-project
pac solution pack --zipfile ./path-to-your-solution/YourSolution.zip --outputdirectory ./path-to-your-solution
```

Deploy to environment
```
pac pcf push --publisher-prefix MyPublisher
```