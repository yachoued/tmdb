CREATE TABLE movies (
    movie_id SERIAL PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    overview TEXT,
    rating DECIMAL(3,1),
    releasedate DATE,
    runtime INTEGER,
    language VARCHAR(50),
    country VARCHAR(100),
    isliked BOOLEAN,
    posterpath VARCHAR(255),
    trailerurl VARCHAR(255)
);

INSERT INTO movies (title, overview, rating, releasedate, runtime, language, country, isliked, posterpath, trailerurl)
VALUES
('Pulp Fiction', 'The lives of two mob hitmen, a boxer, and others intertwine in four tales of violence and redemption.', 8.9, '1994-10-14', 154, 'English', 'USA', TRUE, '/path/pulp_fiction.jpg', 'https://www.youtube.com/watch?v=s7EdQ4FqbhY'),
('Schindlers List', 'In Nazi-occupied Poland, a businessman saves countless Jews from the Holocaust.', 9.0, '1993-12-15', 195, 'English', 'USA', TRUE, '/path/schindlers_list.jpg', 'https://www.youtube.com/watch?v=gG22XNhtnoY'),
('The Lord of the Rings: The Fellowship of the Ring', 'A meek Hobbit and eight companions set out on a journey to destroy the One Ring.', 8.8, '2001-12-19', 178, 'English', 'New Zealand', TRUE, '/path/lotr_fellowship.jpg', 'https://www.youtube.com/watch?v=V75dMMIW2B4'),
('Fight Club', 'An insomniac office worker forms an underground fight club that spirals out of control.', 8.8, '1999-10-15', 139, 'English', 'USA', TRUE, '/path/fight_club.jpg', 'https://www.youtube.com/watch?v=SUXWAEX2jlg'),
('Forrest Gump', 'Forrest Gump, a man with a low IQ, recounts his life story and its impact on American culture.', 8.8, '1994-07-06', 142, 'English', 'USA', TRUE, '/path/forrest_gump.jpg', 'https://www.youtube.com/watch?v=bLvqoHBptjg'),
('The Matrix', 'A computer hacker learns the true nature of his reality and his role in the war against its controllers.', 8.7, '1999-03-31', 136, 'English', 'USA', TRUE, '/path/matrix.jpg', 'https://www.youtube.com/watch?v=vKQi3bBA1y8'),
('Goodfellas', 'The story of Henry Hill and his life in the mob, covering his relationship with his wife and partners.', 8.7, '1990-09-19', 146, 'English', 'USA', TRUE, '/path/goodfellas.jpg', 'https://www.youtube.com/watch?v=2ilzidi_J8Q'),
('The Silence of the Lambs', 'A young FBI cadet must confide in an incarcerated and manipulative killer to catch another serial killer.', 8.6, '1991-02-14', 118, 'English', 'USA', TRUE, '/path/silence_lambs.jpg', 'https://www.youtube.com/watch?v=W6Mm8Sbe__o'),
('Saving Private Ryan', 'Following the Normandy Landings, a group of U.S. soldiers go behind enemy lines to retrieve a paratrooper.', 8.6, '1998-07-24', 169, 'English', 'USA', TRUE, '/path/saving_private_ryan.jpg', 'https://www.youtube.com/watch?v=zwhP5b4tD6g'),
('The Green Mile', 'The lives of guards on Death Row are affected by one of their charges: a man accused of child murder and rape.', 8.6, '1999-12-10', 189, 'English', 'USA', TRUE, '/path/green_mile.jpg', 'https://www.youtube.com/watch?v=Ki4haFrqSrw'),
('The Shawshank Redemption', 'Two imprisoned men bond over several years, finding solace and eventual redemption through acts of common decency.', 9.3, '1994-09-22', 142, 'English', 'USA', TRUE, '/path/shawshank.jpg', 'https://www.youtube.com/watch?v=NmzuHjWmXOc'),
('The Empire Strikes Back', 'After the rebels are overpowered by the Empire on the ice planet Hoth, Luke Skywalker begins Jedi training with Yoda.', 8.7, '1980-05-21', 124, 'English', 'USA', TRUE, '/path/empire_strikes_back.jpg', 'https://www.youtube.com/watch?v=JNwNXF9Y6kY'),
('Gladiator', 'A former Roman general sets out to exact vengeance against the corrupt emperor who murdered his family.', 8.5, '2000-05-05', 155, 'English', 'USA', TRUE, '/path/gladiator.jpg', 'https://www.youtube.com/watch?v=owK1qxDselE'),
('The Departed', 'An undercover cop and a mole in the police attempt to identify each other while infiltrating an Irish gang in Boston.', 8.5, '2006-10-06', 151, 'English', 'USA', TRUE, '/path/departed.jpg', 'https://www.youtube.com/watch?v=iojhqm0JTW4'),
('Whiplash', 'A promising young drummer enrolls at a cut-throat music conservatory, where his dreams of greatness are mentored by an instructor who will stop at nothing to realize a student’s potential.', 8.5, '2014-10-10', 106, 'English', 'USA', TRUE, '/path/whiplash.jpg', 'https://www.youtube.com/watch?v=7d_jQycdQGo');
