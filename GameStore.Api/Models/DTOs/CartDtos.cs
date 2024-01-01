namespace GameStore.Api.Models.DTOs;

public record CartDto(
    int Id,
    double TotalCheckout,
    List<ItemDto> Items
);

public record AddToCartDto(
    List<ItemDto> Items
);

public record ItemDto(
    int Quantity,
    int GameId
);