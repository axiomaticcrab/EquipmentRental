# EquipmentRental

To run the project

A) Open the .sln with Visual Stuio 2017 (as Administrator) and click run(F5). Make sure docker-compose project selected as a startup project.

B)
  1- "cd" to root folder of the application where docker-compose file is located.  
  2- docker-compose up --build
  3- To display ip address of the ui project type "docker inspect -f '{{range .NetworkSettings.Networks}}{{.IPAddress}}{{end}}' equipmentrental_equipmentrental.ui_1"
  4- Open browser and navigate to printed ip address.
  
* Also be sure that docker deamon experimental feautes are enabled.





![Alt text](/Untitled%20Diagram.png?raw=true "Overall Architecture")
