# HydroMeasure

## Description

This project is a simple API that allows to manage the water consumption of a house.

## Installation

To install the project, you need to have the following tools installed:

   dotnet ef migrations add InitialCreate --project HydroMeasure.Infrastructure --startup-project HydroMeasure.Api
   
   dotnet ef database update --project HydroMeasure.Infrastructure --startup-project HydroMeasure.Api
   