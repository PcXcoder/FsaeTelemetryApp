o configure and use the racingTX application, follow these steps:

Edit ip.cfg:

Locate and edit the ip.cfg file in the application's directory.
Set the IP address where the HTTP POST requests will be sent. Additionally, you will find three other variables:
Length of message 1
Length of message 2
Maximum time between messages
Edit template.json:

Find and modify the template.json file in the application's directory.
Adjust the JSON structure as needed to format the data sent via HTTP.
Execute racingTX.exe:

Navigate to the bin directory of the application.
Run racingTX.exe to start the application with your configured settings.
These steps will ensure that the racingTX application is configured to send HTTP POST requests with the specified JSON structure to the designated IP address. Adjust the configurations in ip.cfg and template.json according to your project requirements before running the application.
