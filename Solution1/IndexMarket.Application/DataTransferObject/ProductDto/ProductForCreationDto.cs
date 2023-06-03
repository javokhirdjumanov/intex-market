﻿namespace IndexMarket.Application.DataTransferObject;
public record ProductForCreationDto(
    string? PhotoLink,
    decimal? SalePrice,
    decimal Price,
    int Amount,
    double Height,
    int Depth,
    string category,
    string shape);