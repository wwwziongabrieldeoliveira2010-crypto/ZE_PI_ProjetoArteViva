
-- DROP DATABASE IF EXISTS arteviva;   -- descomente para recriar do zero

CREATE DATABASE IF NOT EXISTS arteviva
    CHARACTER SET utf8mb4;

USE arteviva;


CREATE TABLE IF NOT EXISTS Categorias (
    Id   INT          NOT NULL AUTO_INCREMENT,
    Nome VARCHAR(60)  NOT NULL,
    CONSTRAINT PK_Categorias PRIMARY KEY (Id)
) CHARACTER SET utf8mb4;

CREATE TABLE IF NOT EXISTS Cidades (
    Id   INT          NOT NULL AUTO_INCREMENT,
    Nome VARCHAR(100) NOT NULL,
    Uf   VARCHAR(2)   NOT NULL,
    CONSTRAINT PK_Cidades PRIMARY KEY (Id)
) CHARACTER SET utf8mb4;

CREATE TABLE IF NOT EXISTS Usuarios (
    Id       INT          NOT NULL AUTO_INCREMENT,
    Nome     VARCHAR(100) NOT NULL,
    Email    VARCHAR(150) NOT NULL,
    Senha    VARCHAR(100) NOT NULL,                 
    Tipo     VARCHAR(10)  NOT NULL DEFAULT 'comum',
    CriadoEm DATETIME(6)  NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    CidadeId INT          NOT NULL,
    CONSTRAINT PK_Usuarios PRIMARY KEY (Id),
    CONSTRAINT FK_Usuarios_Cidades_CidadeId FOREIGN KEY (CidadeId)
        REFERENCES Cidades (Id) ON DELETE RESTRICT
) CHARACTER SET utf8mb4;

CREATE TABLE IF NOT EXISTS Eventos (
    Id          INT           NOT NULL AUTO_INCREMENT,
    Titulo      VARCHAR(150)  NOT NULL,
    Descricao   VARCHAR(2000) NOT NULL,
    DataHora    DATETIME(6)   NOT NULL,
    Local       VARCHAR(200)  NOT NULL,
    Gratuito    TINYINT(1)    NOT NULL DEFAULT 1,
    Status      VARCHAR(15)   NOT NULL DEFAULT 'aprovado',
    CriadoEm    DATETIME(6)   NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    CidadeId    INT           NOT NULL,
    CategoriaId INT           NOT NULL,
    UsuarioId   INT           NOT NULL,
    CONSTRAINT PK_Eventos PRIMARY KEY (Id),
    CONSTRAINT FK_Eventos_Cidades_CidadeId FOREIGN KEY (CidadeId)
        REFERENCES Cidades (Id) ON DELETE RESTRICT,
    CONSTRAINT FK_Eventos_Categorias_CategoriaId FOREIGN KEY (CategoriaId)
        REFERENCES Categorias (Id) ON DELETE RESTRICT,
    CONSTRAINT FK_Eventos_Usuarios_UsuarioId FOREIGN KEY (UsuarioId)
        REFERENCES Usuarios (Id) ON DELETE CASCADE      
) CHARACTER SET utf8mb4;


CREATE UNIQUE INDEX IX_Usuarios_Email          ON Usuarios   (Email);                   -- RN01
CREATE UNIQUE INDEX IX_Categorias_Nome         ON Categorias (Nome);
CREATE        INDEX IX_Usuarios_CidadeId       ON Usuarios   (CidadeId);
CREATE        INDEX IX_Eventos_DataHora        ON Eventos    (DataHora);
CREATE        INDEX IX_Eventos_CidadeId        ON Eventos    (CidadeId);
CREATE        INDEX IX_Eventos_CategoriaId     ON Eventos    (CategoriaId);
CREATE        INDEX IX_Eventos_UsuarioId       ON Eventos    (UsuarioId);


INSERT INTO Categorias (Id, Nome) VALUES
    (1, 'Música'),
    (2, 'Teatro'),
    (3, 'Exposições de arte'),
    (4, 'Feiras culturais'),
    (5, 'Dança'),
    (6, 'Cinema'),
    (7, 'Oficinas'),
    (8, 'Outros');

INSERT INTO Cidades (Id, Nome, Uf) VALUES
    (1, 'São José',      'SC'),
    (2, 'Florianópolis', 'SC'),
    (3, 'Palhoça',       'SC'),
    (4, 'Biguaçu',       'SC');

CREATE TABLE IF NOT EXISTS __EFMigrationsHistory (
    MigrationId    VARCHAR(150) NOT NULL,
    ProductVersion VARCHAR(32)  NOT NULL,
    CONSTRAINT PK___EFMigrationsHistory PRIMARY KEY (MigrationId)
) CHARACTER SET utf8mb4;

INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) VALUES
    ('20261115150742_CriacaoInicial', '10.0.12'),
    ('20261116182646_Inicial',        '10.0.12');
    

USE arteviva;


SELECT e.*, c.Nome AS Cidade, k.Nome AS Categoria
FROM Eventos e
JOIN Cidades c    ON c.Id = e.CidadeId
JOIN Categorias k ON k.Id = e.CategoriaId
WHERE e.DataHora >= NOW()
ORDER BY e.DataHora
LIMIT 6;


SELECT e.*, c.Nome AS Cidade, k.Nome AS Categoria
FROM Eventos e
JOIN Cidades c    ON c.Id = e.CidadeId
JOIN Categorias k ON k.Id = e.CategoriaId
WHERE e.Status = 'aprovado'
  AND e.DataHora >= NOW()
  -- AND e.CidadeId = 1
  -- AND e.CategoriaId = 2
ORDER BY e.DataHora;


SELECT e.*, c.Nome AS Cidade, k.Nome AS Categoria
FROM Eventos e
JOIN Cidades c    ON c.Id = e.CidadeId
JOIN Categorias k ON k.Id = e.CategoriaId
WHERE e.UsuarioId = 1
ORDER BY e.DataHora;


DELETE FROM Eventos WHERE Id = 1 AND UsuarioId = 1;


SELECT * FROM Usuarios WHERE Email = 'teste@email.com';


SELECT COUNT(*) FROM Usuarios WHERE Email = 'teste@email.com';

