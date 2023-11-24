﻿using Shopway.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Shopway.Presentation.Authentication.PermissionAuthentication;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
public sealed class HasPermissionAttribute(Permission permission) : AuthorizeAttribute(policy: $"{permission}");
