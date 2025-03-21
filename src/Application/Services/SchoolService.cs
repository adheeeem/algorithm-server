﻿using Application.DTOs;
using Application.Interfaces;
using Application.Requests;
using Application.Responses;
using Domain.Entities;

namespace Application.Services;

public class SchoolService(IUnitOfWork unitOfWork)
{
	public async Task<int> AddNewSchool(CreateSchoolRequest request)
	{
		var schoolDto = new CreateSchoolDto { City = request.City, Country = request.Country, Name = request.Name, Region = request.Region };
		var id = await unitOfWork.SchoolRepository.CreateSchool(schoolDto);
		return id;
	}
	public async Task<ListedResponse<School>> GetSchools(int limit = 0, int page = 0, string name = "", string region = "", string city = "", string country = "")
	{
		var response = new ListedResponse<School>();
		var schools = await unitOfWork.SchoolRepository.GetSchools(limit, page, name, region, city, country);
		var cnt = await unitOfWork.SchoolRepository.GetSchoolCount();
		response.Items = schools;
		response.Total = cnt;
		return response;
	}
}
