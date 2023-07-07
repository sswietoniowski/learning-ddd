﻿using Shopway.Application.CQRS.Products.Commands.AddReview;
using static Shopway.Tests.Integration.Container.Constants.ReviewConstants;

namespace Shopway.Tests.Integration.Container.ControllersUnderTest.ProductController.Utilities;

public static class AddReviewCommandUtility
{
    public static AddReviewCommand.AddReviewRequestBody CreateAddReviewCommand(int? stars = null, string? title = null, string? description = null)
    {
        return new AddReviewCommand.AddReviewRequestBody
        (
            stars ?? Stars,
            title ?? Title,
            description ?? Description
        );
    }
}