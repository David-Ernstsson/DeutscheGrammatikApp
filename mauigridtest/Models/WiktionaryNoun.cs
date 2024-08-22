using CsvHelper.Configuration.Attributes;

namespace mauigridtest.Models;

public class WiktionaryNoun
{
    [Ignore]
    public int Id { get; set; }

    [Name("lemma")]
    public string Lemma { get; set; }

    [Name("pos")]
    public string Pos { get; set; }

    [Name("genus")]
    public string Genus { get; set; }

    [Name("genus 1")]
    public string Genus1 { get; set; }

    [Name("genus 2")]
    public string Genus2 { get; set; }

    [Name("genus 3")]
    public string Genus3 { get; set; }

    [Name("genus 4")]
    public string Genus4 { get; set; }

    [Name("nominativ singular")]
    public string NominativSingular { get; set; }

    [Name("nominativ singular*")]
    public string NominativSingularStar { get; set; }

    [Name("nominativ singular 1")]
    public string NominativSingular1 { get; set; }

    [Name("nominativ singular 2")]
    public string NominativSingular2 { get; set; }

    [Name("nominativ singular 3")]
    public string NominativSingular3 { get; set; }

    [Name("nominativ singular 4")]
    public string NominativSingular4 { get; set; }

    [Name("nominativ singular stark")]
    public string NominativSingularStark { get; set; }

    [Name("nominativ singular schwach")]
    public string NominativSingularSchwach { get; set; }

    [Name("nominativ singular gemischt")]
    public string NominativSingularGemischt { get; set; }

    [Name("nominativ plural")]
    public string NominativPlural { get; set; }

    [Name("nominativ plural*")]
    public string NominativPluralStar { get; set; }

    [Name("nominativ plural 1")]
    public string NominativPlural1 { get; set; }

    [Name("nominativ plural 2")]
    public string NominativPlural2 { get; set; }

    [Name("nominativ plural 3")]
    public string NominativPlural3 { get; set; }

    [Name("nominativ plural 4")]
    public string NominativPlural4 { get; set; }

    [Name("nominativ plural stark")]
    public string NominativPluralStark { get; set; }

    [Name("nominativ plural schwach")]
    public string NominativPluralSchwach { get; set; }

    [Name("nominativ plural gemischt")]
    public string NominativPluralGemischt { get; set; }

    [Name("genitiv singular")]
    public string GenitivSingular { get; set; }

    [Name("genitiv singular*")]
    public string GenitivSingularStar { get; set; }

    [Name("genitiv singular 1")]
    public string GenitivSingular1 { get; set; }

    [Name("genitiv singular 2")]
    public string GenitivSingular2 { get; set; }

    [Name("genitiv singular 3")]
    public string GenitivSingular3 { get; set; }

    [Name("genitiv singular 4")]
    public string GenitivSingular4 { get; set; }

    [Name("genitiv singular stark")]
    public string GenitivSingularStark { get; set; }

    [Name("genitiv singular schwach")]
    public string GenitivSingularSchwach { get; set; }

    [Name("genitiv singular gemischt")]
    public string GenitivSingularGemischt { get; set; }

    [Name("genitiv plural")]
    public string GenitivPlural { get; set; }

    [Name("genitiv plural*")]
    public string GenitivPluralStar { get; set; }

    [Name("genitiv plural 1")]
    public string GenitivPlural1 { get; set; }

    [Name("genitiv plural 2")]
    public string GenitivPlural2 { get; set; }

    [Name("genitiv plural 3")]
    public string GenitivPlural3 { get; set; }

    [Name("genitiv plural 4")]
    public string GenitivPlural4 { get; set; }

    [Name("genitiv plural stark")]
    public string GenitivPluralStark { get; set; }

    [Name("genitiv plural schwach")]
    public string GenitivPluralSchwach { get; set; }

    [Name("genitiv plural gemischt")]
    public string GenitivPluralGemischt { get; set; }

    [Name("dativ singular")]
    public string DativSingular { get; set; }

    [Name("dativ singular*")]
    public string DativSingularStar { get; set; }

    [Name("dativ singular 1")]
    public string DativSingular1 { get; set; }

    [Name("dativ singular 2")]
    public string DativSingular2 { get; set; }

    [Name("dativ singular 3")]
    public string DativSingular3 { get; set; }

    [Name("dativ singular 4")]
    public string DativSingular4 { get; set; }

    [Name("dativ singular stark")]
    public string DativSingularStark { get; set; }

    [Name("dativ singular schwach")]
    public string DativSingularSchwach { get; set; }

    [Name("dativ singular gemischt")]
    public string DativSingularGemischt { get; set; }

    [Name("dativ plural")]
    public string DativPlural { get; set; }

    [Name("dativ plural*")]
    public string DativPluralStar { get; set; }

    [Name("dativ plural 1")]
    public string DativPlural1 { get; set; }

    [Name("dativ plural 2")]
    public string DativPlural2 { get; set; }

    [Name("dativ plural 3")]
    public string DativPlural3 { get; set; }

    [Name("dativ plural 4")]
    public string DativPlural4 { get; set; }

    [Name("dativ plural stark")]
    public string DativPluralStark { get; set; }

    [Name("dativ plural schwach")]
    public string DativPluralSchwach { get; set; }

    [Name("dativ plural gemischt")]
    public string DativPluralGemischt { get; set; }

    [Name("akkusativ singular")]
    public string AkkusativSingular { get; set; }

    [Name("akkusativ singular*")]
    public string AkkusativSingularStar { get; set; }

    [Name("akkusativ singular 1")]
    public string AkkusativSingular1 { get; set; }

    [Name("akkusativ singular 2")]
    public string AkkusativSingular2 { get; set; }

    [Name("akkusativ singular 3")]
    public string AkkusativSingular3 { get; set; }

    [Name("akkusativ singular 4")]
    public string AkkusativSingular4 { get; set; }

    [Name("akkusativ singular stark")]
    public string AkkusativSingularStark { get; set; }

    [Name("akkusativ singular schwach")]
    public string AkkusativSingularSchwach { get; set; }

    [Name("akkusativ singular gemischt")]
    public string AkkusativSingularGemischt { get; set; }

    [Name("akkusativ plural")]
    public string AkkusativPlural { get; set; }

    [Name("akkusativ plural*")]
    public string AkkusativPluralStar { get; set; }

    [Name("akkusativ plural 1")]
    public string AkkusativPlural1 { get; set; }

    [Name("akkusativ plural 2")]
    public string AkkusativPlural2 { get; set; }

    [Name("akkusativ plural 3")]
    public string AkkusativPlural3 { get; set; }

    [Name("akkusativ plural 4")]
    public string AkkusativPlural4 { get; set; }

    [Name("akkusativ plural stark")]
    public string AkkusativPluralStark { get; set; }

    [Name("akkusativ plural schwach")]
    public string AkkusativPluralSchwach { get; set; }

    [Name("akkusativ plural gemischt")]
    public string AkkusativPluralGemischt { get; set; }
}