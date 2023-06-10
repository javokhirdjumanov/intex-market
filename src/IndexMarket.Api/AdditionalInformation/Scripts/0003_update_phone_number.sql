
UPDATE public."Users" as u
  SET "PhoneNumber" = regexp_replace(
    u."PhoneNumber",
    '^[^0-9]|(?<=.)[^0-9]',
    '',
    'g');