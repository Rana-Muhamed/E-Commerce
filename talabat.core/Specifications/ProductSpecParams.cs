﻿namespace Talabat.Core.Specifications;

public class ProductSpecParams
{
	private const int MaxPageSize = 10;
	private int pageSize =5;

	public int PageSize
	{
		get { return pageSize; }
		set { pageSize = value > MaxPageSize ? MaxPageSize : value ; }
	}
	private string search;

	public string Search
	{
		get { return search; }
		set { search = value.ToLower(); }
	}

	public int PageIndex { get; set; } = 1;

	public string? Sort { get; set; }
    public int? BrandId { get; set; }
    public int? TypeId { get; set; }
}
