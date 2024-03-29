﻿namespace CustomerService.Web.Endpoints.ContributorEndpoints;

public class GetContributorByIdRequest
{
  public const string Route = "/Contributors/{ContributorId:Guid}";
  public static string BuildRoute(Guid contributorId) => Route.Replace("{ContributorId:Guid}", contributorId.ToString());

  public Guid ContributorId { get; set; }
}
