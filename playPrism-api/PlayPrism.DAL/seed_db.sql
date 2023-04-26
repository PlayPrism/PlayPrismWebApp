INSERT INTO "ProductCategories" ("Id", "DateCreated", "DateUpdated", "CategoryName")
VALUES ('a1c139f8-1fa8-11ec-9a03-0242ac130003', now(), now(), 'Games'),
       ('a1c13b02-1fa8-11ec-9a03-0242ac130003', now(), now(), 'Software'),
       ('a1c13c0c-1fa8-11ec-9a03-0242ac130003', now(), now(), 'Accounts');


-- seed ProductCategories table
INSERT INTO "Products" ("Id", "DateCreated", "DateUpdated", "Name", "Price", "ProductCategoryId")
VALUES ('a1c10a91-1fa8-11ec-9a03-0242ac130003', now(), now(), 'Super Mario Odyssey', 59.99,
        'a1c139f8-1fa8-11ec-9a03-0242ac130003'),
       ('a1c10c94-1fa8-11ec-9a03-0242ac130003', now(), now(), 'The Legend of Zelda: Breath of the Wild', 59.99,
        'a1c139f8-1fa8-11ec-9a03-0242ac130003'),
       ('a1c10e98-1fa8-11ec-9a03-0242ac130003', now(), now(), 'Adobe Photoshop', 299.99,
        'a1c13b02-1fa8-11ec-9a03-0242ac130003'),
       ('a1c10fa2-1fa8-11ec-9a03-0242ac130003', now(), now(), 'Super Smash Bros. Ultimate', 59.99,
        'a1c13b02-1fa8-11ec-9a03-0242ac130003'),
       ('a1c110ac-1fa8-11ec-9a03-0242ac130003', now(), now(), 'Animal Crossing: New Horizons', 59.99,
        'a1c139f8-1fa8-11ec-9a03-0242ac130003'),
       ('a1c111b6-1fa8-11ec-9a03-0242ac130003', now(), now(), 'Minecraft', 26.95,
        'a1c139f8-1fa8-11ec-9a03-0242ac130003'),
       ('a1c112c0-1fa8-11ec-9a03-0242ac130003', now(), now(), 'Pokémon Sword and Shield', 59.99,
        'a1c139f8-1fa8-11ec-9a03-0242ac130003'),
       ('a1c113ca-1fa8-11ec-9a03-0242ac130003', now(), now(), 'Mario Kart 8 Deluxe', 59.99,
        'a1c139f8-1fa8-11ec-9a03-0242ac130003'),
       ('a1c114d4-1fa8-11ec-9a03-0242ac130003', now(), now(), 'Spotify Premium', 9.99,
        'a1c13c0c-1fa8-11ec-9a03-0242ac130003'),
       ('a1c115de-1fa8-11ec-9a03-0242ac130003', now(), now(), 'Netflix Premium', 18.99,
        'a1c13c0c-1fa8-11ec-9a03-0242ac130003');


-- seed VariationOptions table
INSERT INTO "VariationOptions" ("Id", "DateCreated", "DateUpdated", "Values")
VALUES ('a1c1e3de-1fa8-11ec-9a03-0242ac130003', now(), now(), '{"Action", "Kids"}' --mario
       ),
       ('a1c1e6f2-1fa8-11ec-9a03-0242ac130003', now(), now(), '{"128GB", "256GB", "512GB"}' -- photoshop
       ),
       ('a1c1ea10-1fa8-11ec-9a03-0242ac130003', now(), now(), '{"Windows", "MacOS", "Linux"}' -- photoshop
       );


INSERT INTO "ProductConfigurations" ("Id", "DateCreated", "DateUpdated", "ProductId", "VariationOptionId",
                                     "ConfigurationName")
VALUES ('a1c1e2d4-1fa8-11ec-9a03-0242ac130003', now(), now(), 'a1c10a91-1fa8-11ec-9a03-0242ac130003',
        'a1c1e3de-1fa8-11ec-9a03-0242ac130003', 'Genre'),
       ('a1c1e4e8-1fa8-11ec-9a03-0242ac130003', now(), now(), 'a1c10e98-1fa8-11ec-9a03-0242ac130003', -- photoshop
        'a1c1ea10-1fa8-11ec-9a03-0242ac130003', 'Platform'),
       ('a1c1e4e7-1fa8-11ec-9a03-0242ac130003', now(), now(), 'a1c10a91-1fa8-11ec-9a03-0242ac130003', -- mario
        'a1c1e3de-1fa8-11ec-9a03-0242ac130003', 'Platform'),
       ('a1c1e906-1fa8-11ec-9a03-0242ac130003', now(), now(), 'a1c10e98-1fa8-11ec-9a03-0242ac130003',
        'a1c1e6f2-1fa8-11ec-9a03-0242ac130003', 'Memory');
