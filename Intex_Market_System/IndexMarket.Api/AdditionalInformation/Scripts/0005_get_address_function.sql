create or replace function get_adresses ()
returns table (
		iid uuid,
		country character varying,
		city character varying,
		region character varying,
		street character varying,
		postal_code smallint) 
language plpgsql
as $$
begin
	return query 
		select adress."Id" as iid,
			   adress."Country" as country,
			   adress."City" as city,
			   adress."Region" as region,
			   adress."Street" as street,
			   adress."PostalCode" as postal_code
		from public."Addresses" as adress;
end;$$

SELECT * FROM public.get_adresses ();

