#!/bin/bash
helm repo add simcube https://simcubeltd.github.io/simcube-helm-charts/
helm install bono-employees-infrastructure-sql-data simcube/mssqlserver-2019 --set acceptEula.value=Y --set edition.value=Developer --set sapassword=H@rd2103211943