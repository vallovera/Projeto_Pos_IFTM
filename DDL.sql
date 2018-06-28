CREATE SCHEMA IF NOT EXISTS `musica` DEFAULT CHARACTER SET utf8 ;
USE `musica` ;

CREATE TABLE IF NOT EXISTS `genero` (
  `codGenero` INT NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(45) NULL,
  PRIMARY KEY (`codGenero`))
ENGINE = InnoDB;




CREATE TABLE IF NOT EXISTS `musica` (
  `codMusica` INT NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(45) NULL,
  `letra` TEXT NULL,
  PRIMARY KEY (`codMusica`))
ENGINE = InnoDB;



CREATE TABLE IF NOT EXISTS `musica_genero` (
  `codMusicaGenero` INT NOT NULL AUTO_INCREMENT,
  `codMusica` INT NOT NULL,
  `codGenero` INT NOT NULL,
  PRIMARY KEY (`codMusicaGenero`),
  INDEX `fk_musica_has_genero_genero1_idx` (`codGenero` ASC),
  INDEX `fk_musica_has_genero_musica1_idx` (`codMusica` ASC),
  CONSTRAINT `fk_musica_has_genero_musica1`
    FOREIGN KEY (`codMusica`)
    REFERENCES `musica` (`codMusica`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_musica_has_genero_genero1`
    FOREIGN KEY (`codGenero`)
    REFERENCES `genero` (`codGenero`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;