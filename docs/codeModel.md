# Code model

We have rules to build our codes, if you want to contribute you will have to follow these rules.

[Disclaimer Warning](../README.md#disclaimer)

## ! WARN

If your changes depend on new dependencies for the application, it will take a little more time before we accept your change directly in our code so that we can see if it is really useful to the point where we need more weight (adding new dependencies creates more weight for the project), I hope you understand, thank you!

## Workspace Configuration

Make sure you have this things:

* [NodeJS](https://nodejs.org) installed in your Machine (Recommended: LTS version)
* **MySQL Server** installed in your Machine
* Optional:
  * [Eslint extension](https://marketplace.visualstudio.com/items?itemName=dbaeumer.vscode-eslint) installed in your Visual Studio Code and allow it to check everything (to facilitate coding)

## How to Start coding

* Clone this repository
* Configure the '.env' file:
  * Copy the 'example.env' and paste into the main folder and remove the 'example' from the name
  * Fill the fields:
    * BOT_TOKEN = // the testing bot token
    * TYPEORM_HOST = // database IP
    * TYPEORM_PORT = // database port
    * TYPEORM_DATABASE = // database name
    * TYPEORM_USERNAME = // database username
    * TYPEORM_PASSWORD = // database password
* Install the Dependencies (Yarn or NPM)
  * Using Yarn = 'yarn'
  * Using NPM = 'npm install'
* *Code your alterations and implementions!*
* Run the system using the "start script" configured in package.json and test your alterations (any other attempt to start the bot otherwise will not work)

## How to know if your changes will be approved by our team

* Run the 'lint' script using 'npm run lint' in your favorite shell
* Run the 'build' script using 'npm run build' in your favorite shell
* *if there were no mistakes, you passed!*

### Navigation

> [Back to Main README](../README.md)
> [Back to Security Policy](https://github.com/Gabriel-Paulucci/KurosawaDia/security/policy)

     Copyright © 2020 Gabriel-Paulucci & KuryKat
