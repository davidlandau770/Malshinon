CREATE DATABASE IF NOT EXISTS malshinon;
CREATE TABLE IF NOT EXISTS people
(
    id INT AUTO_INCREMENT PRIMARY KEY,
    first_name VARCHAR(30) NOT NULL,
    last_name VARCHAR(30) NOT NULL,
    secret_code VARCHAR(30) UNIQUE,
    type_role ENUM('reporter','target','reporter and target','potential_agent'),
    num_reports INT DEFAULT 0,
    num_mentions INT DEFAULT 0
);
-- INSERT INTO people (id, first_name, last_name, secret_code, type_role, num_reports, num_mentions) VALUES (0,'david','landau',4678,'reporter',0,0);

CREATE TABLE IF NOT EXISTS Intel_Reports
(
    id INT AUTO_INCREMENT PRIMARY KEY,
    reporter_id INT NOT NULL,
    target_id INT NOT NULL,
    text VARCHAR(200),
    timestamp DATETIME DEFAULT NOW(),
    FOREIGN KEY (reporter_id) REFERENCES people(id),
    FOREIGN KEY (target_id) REFERENCES people(id)
);
-- INSERT INTO Intel_Reports (id, reporter_id, target_id, text, timestamp) VALUES (0,2,3,'dsjhd dflsdfhds fkdsv','0');
