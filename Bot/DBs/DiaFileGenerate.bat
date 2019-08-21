@echo off
title Dia Generate Drop File
color 24

del Dia.sql

copy /b Dia_*_1.sql Temp_1.sql
copy /b Dia_*_2.sql Temp_2.sql
copy /b Dia_*_3.sql Temp_3.sql
copy /b Temp_*.sql Dia.sql
del Temp_*.sql
echo Gerado
pause