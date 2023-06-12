CREATE OR REPLACE FUNCTION public.report_orders_with_quantities(
	from_date date,
	to_date date,
	collect_quantity BOOLEAN,
	given_quentity bigint DEFAULT 0)
RETURNS TABLE (
	product_id UUID,
	category_name CHARACTER VARYING,
	quantity bigint
)
LANGUAGE plpgsql
AS $$
BEGIN
	IF collect_quantity THEN
		RETURN QUERY
			SELECT 
				cr."Id" AS product_id,
				cr."Title" AS category_name,
				CAST(SUM(subquery.quantity) AS bigint) AS quantity
			FROM 
				public."Categories" AS cr
				INNER JOIN (
					SELECT 
						pr."Category_Id",
						CAST(COUNT(*) AS bigint) AS quantity
					FROM 
						public."Orders" AS o
						INNER JOIN public."Products" AS pr ON o."Product_Id" = pr."Id"
					WHERE 
						o."CreatedAt" BETWEEN from_date AND to_date
					GROUP BY 
						pr."Category_Id"
				) AS subquery ON cr."Id" = subquery."Category_Id"
			GROUP BY 
				cr."Id",
				cr."Title"
			HAVING 
				CAST(SUM(subquery.quantity) AS bigint) > COALESCE(given_quentity, 0)
			ORDER BY 
				CAST(SUM(subquery.quantity) AS bigint) DESC;
	ELSE
		RETURN QUERY
			SELECT 
				pr."Id" AS product_id,
				cr."Title" AS category_name,
				CAST(COUNT(*) AS bigint) AS quantity
			FROM 
				public."Orders" AS o
				INNER JOIN public."Products" AS pr ON o."Product_Id" = pr."Id"
				INNER JOIN public."Categories" AS cr ON pr."Category_Id" = cr."Id"
			WHERE 
				o."CreatedAt" BETWEEN from_date AND to_date
			GROUP BY 
				pr."Id",
				cr."Title"
			HAVING 
				COUNT(*) > COALESCE(given_quentity, 0);	
	END IF;
END; 
$$