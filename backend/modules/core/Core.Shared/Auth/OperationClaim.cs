﻿namespace Core.Shared.Auth;

public class OperationClaim
{
    public string ClaimType { get; set; }
    public string Issuer { get; set; }
    public string Value { get; set; }
}
