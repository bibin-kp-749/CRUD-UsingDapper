﻿using CRUD_UsingDapper.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CRUD_UsingDapper.Service
{
    public class StudentService:IStudentService
    {
        private readonly String _connectionString;
        public StudentService(IConfiguration configuration)
        {
            this._connectionString = configuration["ConnectionStrings:DefaultConnection"].ToString();
        }
        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            using(var connection=new SqlConnection(_connectionString))
            {
                if(connection.State==ConnectionState.Closed) 
                    await connection.OpenAsync();
                var response = await connection.QueryAsync<Student>("select * from student");
                return response;
            }
        }
        public async Task<int> AddStudent(StudentDto student)
        {
            using(var connection=new SqlConnection(_connectionString)){
                if(connection.State==ConnectionState.Closed)
                    await connection.OpenAsync();
                var response = await connection.ExecuteAsync("insert into student values(@Name,@Department,@Age)",
                    new {Name=student.name,Department=student.department,Age=student.age}
                );
                return response;
            }
        }
        public async Task<dynamic> UpdateStudent(Student student)
        {
            using(var connection=new SqlConnection(_connectionString))
            {
                if(connection.State==ConnectionState.Closed)
                    await connection.OpenAsync();
                var response = await connection.ExecuteAsync("Update student set name=@Name,department=@Department,@age=Age where id=@Id", new
                {
                    Name = student.name,
                    Department = student.department,
                    Age = student.age,
                    Id=student.id
                });
                return response;
            }
        }
        public async Task DeleteStudent(int id)
        {
            using(var connection=new SqlConnection(_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    await connection.OpenAsync();
                await connection.ExecuteAsync("Delete from student where id=@Id", new
                {
                    Id = id
                });
           
            }
        }
    }
}
