CREATE or REPLACE function get_report_orders(start_date date, end_date date, is_button boolean, given_quentity bigint)
returns table (id uuid, category_name character varying, quentity bigint)
as $$
begin
    return query
    SELECT
        case 
            when is_button = false then pr."Id" 
            else ca."Id" 
        end as id,
        ca."Title",
        COUNT(*) as quentity
    FROM public."Orders" o
    inner join public."Products" pr on o."Product_Id" = pr."Id"
    inner join public."Categories" ca on pr."Category_Id" = ca."Id"
    where o."CreatedAt" between start_date and end_date
    GROUP BY 
        case 
            when is_button = false then pr."Id"
            else ca."Id" 
        end,
        ca."Title"
	HAVING COUNT(*) > COALESCE(given_quentity, 0)
	ORDER BY quentity desc;
end;
$$ language plpgsql;