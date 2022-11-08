﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotel_management
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMongoCollection<Employee> _employees;

        public EmployeeService(IHotelStoreDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _employees = database.GetCollection<Employee>(settings.EmployeeCollectionName);
        }

        public Employee Create(Employee employee)
        {
            _employees.InsertOne(employee);
            return employee;
        }

        public List<Employee> Get()
        {
            return _employees.Find(employee => true).ToList();
        }

        public Employee Get(string id)
        {
            return _employees.Find(employee => employee.Id == id).FirstOrDefault();
        }

        public void Remove(string id)
        {
            _employees.DeleteOne(employee => employee.Id == id);
        }

        public void Update(string id, Employee employee)
        {
            _employees.ReplaceOne(employee => employee.Id == id, employee);
        }
        
        public Employee GetByeEmployeeId(string employeeId)
        {
           return _employees.Find(employee => employee.EmployeeId == employeeId).FirstOrDefault();
        }

   
    }
}