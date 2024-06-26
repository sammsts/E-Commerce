CREATE TABLE Usuarios (
    usu_id SERIAL PRIMARY KEY,
    usu_nome VARCHAR(250) NOT NULL,
    usu_email VARCHAR(250) NOT NULL,
    usu_senhahash BYTEA NOT NULL,
    usu_senhasalt BYTEA NOT NULL,
    usu_status BOOLEAN DEFAULT TRUE NOT NULL
);

--------------------------------------------------------------------
CREATE TABLE enderecos (
    end_id SERIAL PRIMARY KEY,
    usu_id INT NOT NULL,
    end_cep INT,
    end_pais VARCHAR(50),
    end_estado CHAR(2),
    end_bairro VARCHAR(50),
    end_rua VARCHAR(50),
    end_numero INT,
    end_complemento VARCHAR(250),
    CONSTRAINT fk_enderecos_usu_id FOREIGN KEY (usu_id) REFERENCES usuarios (usu_id)
);
--------------------------------------------------------------------
CREATE TABLE produtos (
    prd_id SERIAL PRIMARY KEY,
    prd_descricao VARCHAR(50),
    prd_quantidadeEstoque INT,
    prd_dataHoraCadastro TIMESTAMP WITHOUT TIME ZONE
);
--------------------------------------------------------------------
CREATE TYPE formaPagamento_pedido AS ENUM ('Credito', 'A vista');
CREATE TYPE situacao_pedido AS ENUM ('Em andamento', 'Finalizado', 'Cancelado');

drop type ped_situacao

drop table pedidos

CREATE TABLE pedidos (
    ped_id SERIAL PRIMARY KEY,
    prd_id INT NOT NULL,
    usu_id INT NOT NULL,
    end_id INT NOT NULL,
    ped_quantidade INT,
    ped_formaPagamento formaPagamento_pedido,
    ped_situacao situacao_pedido,
    ped_dataPedido TIMESTAMP WITHOUT TIME ZONE,
    CONSTRAINT fk_pedidos_prd_id FOREIGN KEY (prd_id) REFERENCES produtos (prd_id),
    CONSTRAINT fk_pedidos_usu_id FOREIGN KEY (usu_id) REFERENCES usuarios (usu_id),
    CONSTRAINT fk_pedidos_end_id FOREIGN KEY (end_id) REFERENCES enderecos (end_id)
);
--------------------------------------------------------------------
CREATE TYPE situacao_carrinho AS ENUM ('Aberto', 'Fechado');

CREATE TABLE carrinho (
    car_id SERIAL PRIMARY KEY,
    ped_id INT NOT NULL,
    prd_id INT NOT NULL,
    usu_id INT NOT NULL,
    end_id INT NOT NULL,
    car_situacao situacao_carrinho,
    CONSTRAINT fk_carrinho_ped_id FOREIGN KEY (ped_id) REFERENCES pedidos (ped_id),
    CONSTRAINT fk_carrinho_prd_id FOREIGN KEY (prd_id) REFERENCES produtos (prd_id),
    CONSTRAINT fk_carrinho_usu_id FOREIGN KEY (usu_id) REFERENCES usuarios (usu_id),
    CONSTRAINT fk_carrinho_end_id FOREIGN KEY (end_id) REFERENCES enderecos (end_id)
);
--------------------------------------------------------------------
CREATE TABLE log (
    log_id SERIAL PRIMARY KEY,
    log_idUsuario INT,
    log_nomeUsuario VARCHAR(50),
    log_loginUsuario VARCHAR(50),
    log_descricao VARCHAR(250)
);