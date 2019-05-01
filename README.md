# TigAR 
is an interactive augmented reality map of University of the Pacific, which can be accessed by any compatible smart phone mobile device. Users can use this application to help locate specific buildings on the Pacific campus, along with various other information like class names and historical facts.

# Image Recognition: Vuforia
# GPS Guidence: Mapbox
##### We orignally planned on created a script to handle the GPS detection. We created the script GPS.cs. When we tested the script in real world settings we found the GPS to be inaccurate and issues with the objects appearing. The issues with the objects appearing is that the objects overlayed on the scren but not in relation to the path. If the user moved their phone, then the object would move with them. 
##### We investigated google's GPS API but we ran into a roadblock. The roadblock was that we did not have a credit card to enter into the webpage. Looking into other options we found Mapbox. Mapbox levegares the power of Unity to be able to easlity incorporate AR Foundation. AR Foundation blends both ARCore and ARKit plugins for unity. With AR Foundation, Unity has AR features and prefabs for use in projects. Investigating into Mapbox was difficult at first because of the outdated resources online. After some trails and erros, a project leveraging Mapbox was created. Below we outline the main steps to implement Mapbox into Unity.
##### The first step was learning the pipeline for Mapbox.
# Miscellaneous

