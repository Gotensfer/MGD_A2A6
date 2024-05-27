using System;
using System.Collections.Generic;

[Serializable]
public class Sprites
{
    public string front_default;
}

[Serializable]
public class Ability
{
    public string name;
    public string url;
}

[Serializable]
public class PokemonAbility
{
    public Ability ability;
    public bool is_hidden;
    public int slot;
}

[Serializable]
public class Form
{
    public string name;
    public string url;
}

[Serializable]
public class Type
{
    public string name;
    public string url;
}

[Serializable]
public class PokemonType
{
    public int slot;
    public Type type;
}

[Serializable]
public class Pokemon
{
    public int id;
    public string name;
    public int base_experience;
    public int height;
    public int weight;
    public List<PokemonAbility> abilities;
    public List<Form> forms;
    public List<PokemonType> types;
    public Sprites sprites;
}
