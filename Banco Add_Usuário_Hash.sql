use banco_bandas;
CREATE TABLE IF NOT EXISTS `usuarios` (
  `idusuario` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(100) NOT NULL,
  `senha` varchar(100) NOT NULL,
  PRIMARY KEY (`idusuario`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/////////////////////////////////////////

CREATE DEFINER=`root`@`localhost` PROCEDURE `consultaSenha`(usuario varchar(100), senha varchar(100))
BEGIN
Select * from usuarios where usuarios.nome = usuario and usuarios.senha = senha;
END;

/////////////////////////////////

CREATE DEFINER=`root`@`localhost` PROCEDURE `insere_user`(in nome varchar(100), in senha varchar(100))
BEGIN
INSERT INTO `usuarios`
(`nome`,
`senha`)
VALUES
(nome,
senha);
END
