# EquipmentRental

To run the project

A) Open the .sln with Visual Stuio 2017 (as Administrator) and click run(F5). Make sure docker-compose project selected as a startup project.

B) <br/>
  1- "cd" to root folder of the application where docker-compose file is located.  <br/>
  2- docker-compose up --build <br/>
  3- To display ip address of the ui project type "docker inspect -f '{{range .NetworkSettings.Networks}}{{.IPAddress}}{{end}}' equipmentrental_equipmentrental.ui_1" <br/>
  4- Open browser and navigate to printed ip address. <br/>
  
* Also be sure that docker deamon experimental feautes are enabled.





![Alt text](/Untitled%20Diagram.png?raw=true "Overall Architecture")
