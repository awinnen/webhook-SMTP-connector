# [webhook-SMTP-connector](https://github.com/awinnen/webhook-SMTP-connector)

webhook-SMTP-connector is a simple Microservice providing the ability to send Emails via SMTP by POSTing the Email as JSON to the API. Useful for Software not able to send Emails directly but provide webhook functionality.

## Installation with Docker
Docker Container available [here](https://hub.docker.com/repository/docker/awinnen/webhook-smtp-connector).

### 1. Create Environment file
Create a file (e.g. smtp.env) with the SMTPHosts at any location on your machine

```
SMTPHosts__0__Config__Host=smtp.server.com
SMTPHosts__0__Config__Password=yourSmtpHostPassword>>
SMTPHosts__0__Config__Port=25
SMTPHosts__0__Config__UseSTARTTLS=true
SMTPHosts__0__Config__Username=yourSmtpHostUsername>>
SMTPHosts__0__Secret=yourDesiredSecret
```
You can add multiple hosts by multiplying these lines and replace the 0 with the corresponsing index
Since this Microservice uses System.Net.Mail.smtpclient, **Implicit SSL is not supported**. We rely on STARTTLS, so make sure to provide the **correct Port for Explicit SSL Support**

### 2. Start Container
```
docker run -p 80:80 --env-file ./smtp.env awinnen/webhook-smtp-connector
```
Make sure to provide the correct path for the environment File created in Step 1


## Usage

POST the following JSON structure to http://localhost:80/send?secret=yourDesiredSecret

Don't forget to set Header `Content-Type = application/json`
```
{
"from": {
	"name": "Sender",
	"emailAddress": "sender@email.com"
	},
"to": {
	"name": "Receiver",
	"emailAddress": "receiver@email.com"
	},
"body": "This is a test email send through webhook-SMTP-connector",
"subject": "webhook test email"
}
```

The SMTPHost used is determined by the secret provided as Query param. If no Config matches, you'll receive an Error Statuscode.
Emails will be sent as HTML, so you can use HTML in body. Keep in Mind to escape any needed characters in the json body.

## TODOS

 1. Improve Logging
 2. Support Implicit SSL
 3. Simple administration interface to add ability to add SMTPHosts at runtime

   

