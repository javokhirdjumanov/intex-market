
SELECT *
    FROM Orders AS o
        INNER JOIN Users AS u ON o.UserId = u.Id
        INNER JOIN Products AS p ON o.ProductId = p.Id
        INNER JOIN ProductShapes AS ps ON p.ProductShapeId = ps.Id
    WHERE LOWER(u.FirstName) LIKE '%' | LOWER(columnName) | '%' OR
          u.PhoneNumber LIKE '%' | columnName | '%' OR
          LOWER(ps.Name) LIKE '%' | LOWER(columnName) | '%';