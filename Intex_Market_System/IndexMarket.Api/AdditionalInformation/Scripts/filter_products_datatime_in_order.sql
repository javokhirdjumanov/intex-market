﻿create or replace function filter_orders_by_product_datatime_model(
	from_date date, to_date date
)
returns table (
	order_id uuid,		
	user_name character varying,
	file_id uuid,
	file_type text,
	file_name text,
	phone_number character varying,
	height double precision,
	weight double precision,
	depths integer,
	price numeric,
	address_id uuid,
	country character varying,
	city character varying,
	region character varying,
	street character varying,
	postal_code smallint,
	create_at timestamp with time zone
) 
language plpgsql
as $$
	begin
		return query 
			SELECT
			o."Id" as order_id,
			u."FirstName" as user_name,
			f."Id" as file_id,
			f."Type" as file_type,
			f."FileName" as file_name,
			u."PhoneNumber" as phone_number,
			pr."Height" as height,
			pr."Weight" as weight,
			pr."Depth" as depths,
			pr."Price" as price,
			ad."Id" as address_id,
			ad."Country" as country,
			ad."City" as city,
			ad."Region" as region,
			ad."Street" as street,
			ad."PostalCode" as postal_code,
			o."CreatedAt" as create_at
		FROM
			public."Orders" as o
			JOIN public."Users" as u ON o."Client_Id" = u."Id"
			JOIN public."Addresses" as ad ON u."Address_Id" = ad."Id"
			JOIN public."Products" as pr ON o."Product_Id" = pr."Id"
			JOIN public."Files" as f ON pr."File_Id" = f."Id"
		WHERE
			o."CreatedAt"::date BETWEEN from_date AND to_date;
	end;$$
	
SELECT * FROM public.filter_orders_by_product_datatime_model('2023-06-07', '2023-06-10');