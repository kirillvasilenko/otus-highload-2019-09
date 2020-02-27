terraform {
  required_version = ">= 0.12, < 0.13"

  backend "s3" {
    bucket = "terraform-up-and-running-kirill-amurskiy-state"
    region = "eu-north-1"
    dynamodb_table = "terrafrom-up-and-running-locks"
    encrypt = true
    key = "otus/hl/3/terraform.tfstate"
  }
}

provider "aws" {
  region = "eu-north-1"

  # Allow any 2.x version of the AWS provider
  version = "~> 2.0"
}

resource "aws_instance" "socnet-app-instance" {
  ami                    = "ami-0b2e6fbf8c9364ab6"
  instance_type          = "t3.micro"
  availability_zone      = "eu-north-1a"
  vpc_security_group_ids = [aws_security_group.socnet-common-sg.id]
  
  tags = {
    Name = "socnet-app-instance"
  }
}

resource "aws_instance" "socnet-dbmaster-instance" {
  ami                    = "ami-0b2e6fbf8c9364ab6"
  instance_type          = "t3.medium"
  availability_zone      = "eu-north-1a"
  vpc_security_group_ids = [aws_security_group.socnet-common-sg.id]

  tags = {
    Name = "socnet-dbmaster-instance"
  }
}

resource "aws_instance" "socnet-dbslave-instance" {
  ami                    = "ami-0b2e6fbf8c9364ab6"
  instance_type          = "t3.micro"
  availability_zone      = "eu-north-1a"
  vpc_security_group_ids = [aws_security_group.socnet-common-sg.id]

  tags = {
    Name = "socnet-dbslave1-instance"
  }
}

resource "aws_instance" "socnet-dbslave2-instance" {
  ami                    = "ami-0b2e6fbf8c9364ab6"
  instance_type          = "t3.micro"
  availability_zone      = "eu-north-1a"
  vpc_security_group_ids = [aws_security_group.socnet-common-sg.id]

  tags = {
    Name = "socnet-dbslave2-instance"
  }
}

resource "aws_security_group" "socnet-common-sg" {
  name = "socnet-common-sg"

  ingress {
    from_port   = 5010
    to_port     = 5010
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    from_port   = 5011
    to_port     = 5011
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    from_port   = 5020
    to_port     = 5020
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    from_port   = 3306
    to_port     = 3306
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    from_port   = 6032
    to_port     = 6032
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    from_port   = 6033
    to_port     = 6033
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    from_port   = 80
    to_port     = 80
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    from_port   = 22
    to_port     = 22
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}